using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nintenlord.Collection;

namespace Nintenlord.Graph.PathFinding
{
    static public class Dijkstra_algorithm
    {
        static public List<TNode> GetArea<TNode>(TNode toStartFrom, IWeighedGraph<TNode> map,
            IEqualityComparer<TNode> nodeComparer, int movementLimit)
        {
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new HashSet<TNode>(nodeComparer);
            ICostCollection<TNode> costs = map.GetTempCostCollection();

            costs[toStartFrom] = 0;
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                int cost;
                TNode node = open.Dequeue(out cost);
                closed.Add(node);

                if (cost < movementLimit)
                {
                    foreach (TNode neighbour in map.GetNeighbours(node))
                    {
                        int newCost = cost + map.GetMovementCost(node, neighbour);
                        int oldCost;
                        if (!costs.TryGetValue(neighbour, out oldCost))
                        {
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                        else if (newCost < oldCost)
                        {
                            open.Remove(neighbour, oldCost);
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                    }
                }
            }

            List<TNode> result = new List<TNode>(closed.Count);
            foreach (TNode node in closed)
            {
                if (costs[node] < movementLimit)
                {
                    result.Add(node);
                }
            }
            costs.Release();
            return result;
        }
        
        static public int GetCost<TNode>(TNode toStartFrom, TNode toEnd, 
            IWeighedGraph<TNode> map, IEqualityComparer<TNode> nodeComparer)
        {
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new HashSet<TNode>(nodeComparer);
            ICostCollection<TNode> costs = map.GetTempCostCollection();

            costs[toStartFrom] = 0;
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                int cost;
                TNode node = open.Dequeue(out cost);
                closed.Add(node);
                int endCost;
                if (!costs.TryGetValue(toEnd, out endCost) || cost < endCost)
                {
                    foreach (TNode neighbour in map.GetNeighbours(node))
                    {
                        int newCost = cost + map.GetMovementCost(node, neighbour);
                        int oldCost;
                        if (!costs.TryGetValue(neighbour, out oldCost))
                        {
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }
                        else if (newCost < oldCost)
                        {
                            open.Remove(neighbour, oldCost);
                            open.Enqueue(neighbour, newCost);
                            costs[neighbour] = newCost;
                        }//http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html
                    }
                }            
            }

            if (closed.Contains(toEnd))
            {
                return costs[toEnd];
            }
            else
            {
                return int.MinValue;
            }
        }

        static public IDictionary<TNode, int> GetCosts<TNode>(TNode toStartFrom,
            IWeighedGraph<TNode> map, IEqualityComparer<TNode> nodeComparer)
        {
            IPriorityQueue<int, TNode> open = new SkipListPriorityQueue<int, TNode>(10);
            HashSet<TNode> closed = new HashSet<TNode>(nodeComparer);
            IDictionary<TNode, int> costs = new Dictionary<TNode, int>(nodeComparer);

            costs[toStartFrom] = 0;
            open.Enqueue(toStartFrom, 0);

            while (open.Count > 0)
            {
                int cost;
                TNode node = open.Dequeue(out cost);
                closed.Add(node);

                foreach (TNode neighbour in map.GetNeighbours(node))
                {
                    int newCost = cost + map.GetMovementCost(node, neighbour);
                    int oldCost;
                    if (!costs.TryGetValue(neighbour, out oldCost))
                    {
                        open.Enqueue(neighbour, newCost);
                        costs[neighbour] = newCost;
                    }
                    else if (newCost < oldCost)
                    {
                        open.Remove(neighbour, oldCost);
                        open.Enqueue(neighbour, newCost);
                        costs[neighbour] = newCost;
                    }//http://theory.stanford.edu/~amitp/GameProgramming/ImplementationNotes.html
                }
            }
            return costs;
        }
    }
}
