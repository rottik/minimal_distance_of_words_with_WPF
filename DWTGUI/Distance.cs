using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWTGUI
{
    class Distance
    {
        static public Dictionary<string, double> n_nearest_words(string word, IEnumerable<string> wordlist, int limit)
        {
            string result = "";
            double minDist = double.MaxValue;
            var best= new Dictionary<string,double>();
            string key = "";
            for (int i = 0; i < limit; i++)
            {
                key+=" ";
                best.Add(key, double.MaxValue);
            }

            foreach (string refWord in wordlist)
            {
                double[,] A = new double[refWord.Length + 1, word.Length + 1];

                A[0, 0] = 0;
                for (int i = 1; i < refWord.Length + 1; i++)
                {
                    A[i, 0] = double.MaxValue;
                }
                int j;
                for (j = 1; j < word.Length + 1; j++)
                {
                    A[0, j] = double.MaxValue;
                }

                int minI = 1;
                j = 1;
                while (j < word.Length)
                {
                    bool nahoru = false;
                    int i = minI;
                    while (i < refWord.Length)
                    {
                        double newWord = (i == 0 && j == 0) ? 0 : double.MaxValue;
                        double over = 1 * A[i - 1, j];
                        double right = 1 * A[i, j - 1];
                        double rightOver = 1 * A[i - 1, j - 1];

                        //smer posunu ve slovo
                        newWord = right;

                        //smer posunu v referenci
                        if (newWord > over)
                        {
                            newWord = over;
                            if (i == minI)
                                nahoru = true;
                        }

                        //smer posunu v obou
                        if (newWord > rightOver)
                            newWord = rightOver;

                        A[i, j] = (refWord[i - 1] == word[j - 1] ? 0 : 1) + newWord;
                        i++;
                    }
                    if (nahoru) { minI++; }
                    j++;
                }
                if (A[refWord.Length - 1, word.Length - 1] < minDist)
                {
                    best.Add(refWord, A[refWord.Length - 1, word.Length - 1]);
                    best.Remove(best.OrderByDescending(p => p.Value).First().Key); 
                    minDist = best.Values.Max();
                }
            }
            return best;
        }
    }
}
