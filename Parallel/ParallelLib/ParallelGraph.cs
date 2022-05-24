using ParallelLib.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelLib
{
    public class ParallelGraph
    {
        private readonly IGraph graph;
        private int[] tiers;

        public ParallelGraph(IGraph graph)
        {
            this.graph = graph;
            tiers = null;
        }

        public int[] GetAlgorithmTiers
        {
            get
            {
                if (tiers is null)
                {
                    BuildParallelForm();
                }
                return tiers;
            }
        }

        private void BuildParallelForm()
        {
            tiers = new int[graph.Size];

            for (int i = 0; i < graph.Size; i++)
            {
                bool isFirstTier = true;
                for (int j = 0; j < graph.Size; j++)
                {
                    if (graph.HasPath(j, i))
                    {
                        isFirstTier = false;
                        break;
                    }
                }
                if (isFirstTier)
                {
                    tiers[i] = 1;
                }
            }

            for (int currentTier = 2; currentTier <= graph.Size; currentTier++)
            {
                for (int i = 0; i < graph.Size; i++)
                {
                    if (tiers[i] > 0)
                    {
                        continue;
                    }
                    bool isTier = true;
                    for (int j = 0; j < graph.Size; j++)
                    {
                        if (graph.HasPath(j, i) && (tiers[j] == 0 || tiers[j] == currentTier))
                        {
                            isTier = false;
                            break;
                        }
                    }
                    if (isTier)
                    {
                        tiers[i] = currentTier;
                    }
                }
            }
        }

    }
}