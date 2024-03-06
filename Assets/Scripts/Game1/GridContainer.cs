using System;
using Unity.Mathematics;
using UnityEngine;

namespace Game1
{
    public class GridContainer : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private Node nodePrefab;
        [SerializeField] private Node Selected;

        public static readonly int[,] grid = new int[,]
        {
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
        };

        public Node[,] NodeGrid;
        private float horizontalOffset;
        private float verticalOffset;

        public void Start()
        {
            SpawnNodes();
        }

        public void SpawnNodes()
        {
            horizontalOffset = grid.GetLength(1) / 2 * offset.x;
            verticalOffset = grid.GetLength(0) / 2 * offset.y;
            NodeGrid = new Node[grid.GetLength(0), grid.GetLength(1)];
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] == 1)
                    {
                        var newNode = Instantiate(nodePrefab,
                            new Vector3(x * offset.x - horizontalOffset, y * offset.y - verticalOffset, 0),
                            quaternion.identity,
                            transform);
                        newNode.Randomize();
                        newNode.Click += NodeOnClick;
                        NodeGrid[y, x] = newNode;
                    }
                }
            }

            var t = "";
            for (int y = 0; y < NodeGrid.GetLength(0); y++)
            {
                for (int x = 0; x < NodeGrid.GetLength(1); x++)
                {
                    t += $" |{(NodeGrid[y, x] == null ? 0 : 1)}| ";
                }

                t += "\n";
            }

            Debug.Log(t);
        }

        private void NodeOnClick(Node obj)
        {
            if (Selected == null)
            {
                Selected = obj;
                Selected.Select();
            }
            else
            {
                var s1Position = GetPosition(Selected);
                var s2Position = GetPosition(obj);
                Swap(Selected, obj);
                Selected.Diselect();
                Selected = null;
                CheckStash();
                Gravity();
            }
        }

        /*
            { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
            { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
         */
        [ContextMenu("test")]
        public void CheckStash()
        {
            //====== вправо 

            // for (int i = 0; i < NodeGrid.GetLength(1); i++)
            // {
            //     var x = i;
            //     for (int j = 0; j < NodeGrid.GetLength(0); j++)
            //     {
            //         var y = j;
            //         if (x >= NodeGrid.GetLength(1))
            //         {
            //             break;
            //         }
            //
            //         Debug.Log($"{x} - {y}");
            //         NodeGrid[y, x] = t++;
            //         x += 1;
            //     }
            // }
            //
            // for (int j = 1; j < NodeGrid.GetLength(0); j++)
            // {
            //     var y = j;
            //
            //     for (int i = 0; i < NodeGrid.GetLength(1); i++)
            //     {
            //         var x = i;
            //         if (y >= NodeGrid.GetLength(0))
            //         {
            //             break;
            //         }
            //
            //         Debug.Log($"{x} - {y}");
            //         NodeGrid[y, x] = t++;
            //         y += 1;
            //    
            //     }
            // }

            //==== Влево
            for (int i = NodeGrid.GetLength(1) - 1; i >= 0; i--)
            {
                var x = i;
                var match = new Match();
                for (int j = 0; j < NodeGrid.GetLength(0); j++)
                {
                    var y = j;
                    if (x < 0)
                    {
                        break;
                    }


                    match.Add(NodeGrid[y, x]);
                    x -= 1;
                }

                match.Use();
            }

            for (int j = 1; j < NodeGrid.GetLength(0); j++)
            {
                var y = j;
                var match = new Match();
                for (int i = 0; i < NodeGrid.GetLength(1); i++)
                {
                    var x = NodeGrid.GetLength(1) - i - 1;
                    if (y >= NodeGrid.GetLength(0))
                    {
                        break;
                    }

                    Debug.Log($"{x} - {y}");
                    match.Add(NodeGrid[y, x]);
                    y += 1;
                }

                match.Use();
            }

            for (var y = 0; y < NodeGrid.GetLength(0); y++)
            {
                for (var x = 0; x < NodeGrid.GetLength(1); x++)
                {
                    if (NodeGrid[y, x] == null) continue;
                    if (!NodeGrid[y, x].isSetToDestroy) continue;
                    var t = NodeGrid[y, x];
                    NodeGrid[y, x] = null;
                    Destroy(t.gameObject);
                }
            }
        }

        public void Gravity()
        {
            
            for (var y = 0; y < NodeGrid.GetLength(0); y++)
            {
                for (var x = 0; x < NodeGrid.GetLength(1); x++)
                {
                    if (NodeGrid[y, x] == null) continue;
                    if (!CheckPosition(new Vector2Int(x - 1, y - 1))) continue; // Swap
                    //NodeGrid[y, x].transform.
                    NodeGrid[y, x].transform.position = new Vector3(x * offset.x - horizontalOffset,
                        y * offset.y - verticalOffset, 0);
                    (NodeGrid[y, x], NodeGrid[y + 1, x - 1]) = (NodeGrid[y + 1, x - 1], NodeGrid[y, x]);
                }
            }

            var t="";
            for (int y = 0; y < NodeGrid.GetLength(0); y++)
            {
                for (int x = 0; x < NodeGrid.GetLength(1); x++)
                {
                    t += $" |{(NodeGrid[y, x] == null ? 0 : 1)}| ";
                }

                t += "\n";
            }
            Debug.Log(t);
        }

        public bool CheckPosition(Vector2Int position)
        {
            if (position.x >= NodeGrid.GetLength(1)) return false;
            if (position.y >= NodeGrid.GetLength(0)) return false;
            if (position.x < 0) return false;
            if (position.y < 0) return false;
            return NodeGrid[position.y, position.x] == null;
        }

        public void Swap(Node s1, Node s2)
        {
            var s1Position = GetPosition(s1);
            var s2Position = GetPosition(s2);
            NodeGrid[s1Position.x, s1Position.y] = s2;
            NodeGrid[s2Position.x, s2Position.y] = s1;
            (s1.transform.position, s2.transform.position) = (s2.transform.position, s1.transform.position);
        }

        private Vector2Int GetPosition(Node node)
        {
            for (int y = 0; y < NodeGrid.GetLength(0); y++)
            {
                for (int x = 0; x < NodeGrid.GetLength(1); x++)
                {
                    if (NodeGrid[y, x] == node)
                    {
                        return new Vector2Int(y, x);
                    }
                }
            }

            return new Vector2Int(100, 100);
        }
    }
}