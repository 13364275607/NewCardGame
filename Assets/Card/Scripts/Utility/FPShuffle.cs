using System;
using System.Collections;
using System.Collections.Generic;
public class FPShuffle
{

		private static Random random = new Random ();
		public static void Shuffle<T> (List<T> listToShuffle, int numberOfTimesToShuffle = 3)
		{
				List<T> newList = new List<T> ();
				for (int i = 0; i < numberOfTimesToShuffle; i++) {

						while (listToShuffle.Count > 0) {

								int index = random.Next (listToShuffle.Count);
								newList.Add (listToShuffle [index]);
								listToShuffle.RemoveAt (index);
						}
						listToShuffle.AddRange (newList);
						newList.Clear ();
				}
		}
		public static void RandomPairsShuffle (List<Level.Pair> pairs, int maxIndex)
		{
				List<int> indexes = new List<int> ();
				for (int i = 0; i < maxIndex; i++) {
						indexes.Add (i);
				}
				Shuffle (indexes, 2);
		
				int current = 0;
				foreach (Level.Pair pair in pairs) {
						pair.firstElement.index = indexes [current];
						current++;
						pair.secondElement.index = indexes [current];
						current++;
				}
		}
}
