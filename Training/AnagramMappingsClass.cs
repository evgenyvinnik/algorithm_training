using System;
using System.Collections.Generic;
using System.Text;

namespace Training
{
    class AnagramMappingsClass
    {
        public int[] AnagramMappings(int[] A, int[] B)
        {
            int[] mapping = new int[A.Length];
            for (int i = 0; i < mapping.Length; ++i)
            {
                mapping[i] = -1;
            }

            for(int i = 0; i < A.Length; ++i)
            {
                for(int j = 0; j < B.Length; ++j)
                {
                    if(A[i] == B[j] && mapping[i] == -1)
                    {
                        mapping[i] = j;
                    }
                }
            }

            return mapping;
        }
    }
}
