using System;
using System.Collections.Generic;
using System.Text;

namespace SabberStoneCoreAi.src
{
    public class Individual
    {
		private List<int> Weights = new List<int>();

		private int Index = -1;

		public Individual()
		{
			initializeWeights();
		}

		public Individual(int index)
		{
			initializeWeights();
			Index = index;
		}

		public Individual(List<int> values)
		{
			Weights = values;
		}

		public void initializeWeights()
		{
			for(int i = 0; i < 11; i++)
			{
				Weights.Add(0);
			}
		}

		public void addWeight(int value)
		{
			Weights.Add(value);
		}

		public int getWeight(int index)
		{
			return Weights[index];
		}

		public int getWeightsLength()
		{
			return Weights.Count;
		}

		public void manipulateWeight(int index, int value)
		{
			Weights[index] += value;
		}

		public void removeWeight(int index)
		{
			Weights.Remove(index);
		}

		public void setWeight(int index, int value)
		{
			Weights[index] = value;
		}

		public void setIndex (int index)
		{
			Index = index;
		}

		public int getIndex()
		{
			return Index;
		}

	}
}
