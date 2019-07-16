using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SabberStoneCoreAi.src
{
    class EvoAlg
    {

		//individual[] gets initialized with random values for the weights
		public Individual[] initializeTrainRun(int numberOfIndividuals, Random generator)
		{
			Individual[] initializaion = new Individual[numberOfIndividuals];

			for(int i = 0;  i < initializaion.Length; i++)
			{
				initializaion[i] = new Individual(i);

				for(int j = 0; j < 11; j++)
				{
					initializaion[i].setWeight(j, generator.Next(-1000, 1001));
				}
			}

			return initializaion;
		}

		//5 crossover for the evolutionary algorithms
		/*
		5 crossover for the evolutionary algorithms
		returns new population with double the size of the old one
		*/
		public Individual[] Crossover(Individual[] oldPop)
		{
			//placeholder for the children
			Individual[] children = new Individual[oldPop.Length * 2];

			int[] crossoverOnePoint = { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
			int[] crossoverTwoPoint = { 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0 };
			int[] crossoverSecond = { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 };
			int[] crossover5Split = { 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0 };
			int[] crossoverUniform = { 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1 };

			//initialize childrens with default weights
			for (int i = 0; i < children.Length; i++)
			{
				children[i] = new Individual(i);
			}

			for (int i = 0; i < (oldPop.Length / 5); i += 2)
			{
				//select parents
				Individual indi1 = oldPop[i];
				Individual indi2 = oldPop[i + 1];

				for (int j = 0; j < crossoverOnePoint.Length; j++)
				{
					children[i * 20].setWeight(j, (crossoverOnePoint[j] == 0) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20].setIndex(i * 20);
					children[i * 20 + 1].setWeight(j, (crossoverOnePoint[j] == 0) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 1].setIndex(i * 20 + 1);
					children[i * 20 + 2].setWeight(j, (crossoverOnePoint[j] == 1) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 2].setIndex(i * 20 + 2);
					children[i * 20 + 3].setWeight(j, (crossoverOnePoint[j] == 1) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 3].setIndex(i * 20 + 3);
					children[i * 20 + 4].setWeight(j, (crossoverTwoPoint[j] == 0) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 4].setIndex(i * 20 + 4);
					children[i * 20 + 5].setWeight(j, (crossoverTwoPoint[j] == 0) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 5].setIndex(i * 20 + 5);
					children[i * 20 + 6].setWeight(j, (crossoverTwoPoint[j] == 1) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 6].setIndex(i * 20 + 6);
					children[i * 20 + 7].setWeight(j, (crossoverTwoPoint[j] == 1) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 7].setIndex(i * 20 + 7);
					children[i * 20 + 8].setWeight(j, (crossoverSecond[j] == 0) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 8].setIndex(i * 20 + 8);
					children[i * 20 + 9].setWeight(j, (crossoverSecond[j] == 0) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 9].setIndex(i * 20 + 9);
					children[i * 20 + 10].setWeight(j, (crossoverSecond[j] == 1) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 10].setIndex(i * 20 + 10);
					children[i * 20 + 11].setWeight(j, (crossoverSecond[j] == 1) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 11].setIndex(i * 20 + 11);
					children[i * 20 + 12].setWeight(j, (crossover5Split[j] == 0) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 12].setIndex(i * 20 + 12);
					children[i * 20 + 13].setWeight(j, (crossover5Split[j] == 0) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 13].setIndex(i * 20 + 13);
					children[i * 20 + 14].setWeight(j, (crossover5Split[j] == 1) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 14].setIndex(i * 20 + 14);
					children[i * 20 + 15].setWeight(j, (crossover5Split[j] == 1) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 15].setIndex(i * 20 + 15);
					children[i * 20 + 16].setWeight(j, (crossoverUniform[j] == 0) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 16].setIndex(i * 20 + 16);
					children[i * 20 + 17].setWeight(j, (crossoverUniform[j] == 0) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 17].setIndex(i * 20 + 17);
					children[i * 20 + 18].setWeight(j, (crossoverUniform[j] == 1) ? indi1.getWeight(j) : indi2.getWeight(j));
					children[i * 20 + 18].setIndex(i * 20 + 18);
					children[i * 20 + 19].setWeight(j, (crossoverUniform[j] == 1) ? indi2.getWeight(j) : indi1.getWeight(j));
					children[i * 20 + 19].setIndex(i * 20 + 19);
				}
			}

			return children;
		}

		//mutate 10 percent of the new population
		public Individual[] Mutation(Individual[] individuals, Random generator)
		{
			for (int i = 0; i < individuals.Length / 10; i++)
			{
				int chosen = generator.Next(0, individuals.Length);
				int gene = generator.Next(0, individuals[0].getWeightsLength());
				int factor = generator.Next(-10, 11);

				individuals[chosen].manipulateWeight(gene, factor);
			}

			return individuals;
		}

		/*
		 select the population for the next iteration
		 with 20 percent elites of teh old population, if new population is worse
		*/
		public Individual[] Selection(Individual[] oldPop, Individual[] newPop)
		{
			Individual[] selection = new Individual[oldPop.Length];
			Individual[] allIndis = new Individual[oldPop.Length/5+newPop.Length];

			for(int i = 0; i < allIndis.Length; i++)
			{
				allIndis[i] = new Individual(i);
				if(i < oldPop.Length/5)
					allIndis[i] = oldPop[i];
				else
				{
					allIndis[i] = newPop[i - (oldPop.Length/5)];
					allIndis[i].setIndex(i);
				}
			}

			allIndis = Sort(allIndis);

			for (int i = 0; i < oldPop.Length; i++)
			{
				selection[i] = allIndis[i];
				selection[i].setIndex(i);
			}

			return selection;
		}

		private static Individual[] Sort(Individual[] population)
		{
			int best = -1;
			Individual bestIndi = new Individual();
			Individual[] temp = new Individual[population.Length];

			for (int i = 0; i < population.Length; i++)
			{
				foreach (Individual indi in population)
				{

					if (indi.getWeight(11) > best)
					{
						best = indi.getWeight(11);
						bestIndi = indi;
					}
				}

				temp[i] = population[bestIndi.getIndex()];
				best = -1;
			}

			return temp;
		}

		public static void Log(string logMessage, TextWriter w)
		{
			w.Write("\r\nLog Entry : ");
			w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
				DateTime.Now.ToLongDateString());
			w.WriteLine("  :{0}", logMessage);
			w.WriteLine("-------------------------------");
		}

	}
}
