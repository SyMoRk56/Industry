using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;

namespace Industry.Tiles.Pipes
{
    public static class PipeNetwork
    {
        public static bool TryFindOutput(Point16 start, out ItemPipeOutputEntity output)
        {
            output = null;

            HashSet<Point16> visited = new();
            Queue<Point16> queue = new();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Main.NewText("1");
                Point16 p = queue.Dequeue();

                if (!visited.Add(p))
                    continue;

                // Проверяем, есть ли TileEntity на этой позиции
                if (TileEntity.ByPosition.TryGetValue(p, out var te))
                {
                    Main.NewText("2");

                    // Если это выходная труба — нашли цель
                    if (te is ItemPipeOutputEntity outPipe)
                    {
                        output = outPipe;
                        Main.NewText($"Найден выход: {p}");
                        return true;
                    }

                    // Если это обычная труба — продолжаем обход
                    if (te is ItemPipeEntity)
                    {
                        Main.NewText("3");

                        foreach (Point16 next in GetNeighbors(p))
                        {
                            // Проверяем, есть ли соседняя труба
                            if (!visited.Contains(next) &&
                                TileEntity.ByPosition.TryGetValue(next, out var neighborTE) &&
                                (neighborTE is ItemPipeEntity || neighborTE is ItemPipeOutputEntity))
                            {
                                queue.Enqueue(next);
                                Main.NewText($"Добавил в очередь {next}");
                            }
                        }
                    }
                }
            }
            Main.NewText("END");
            return false;
        }



        private static IEnumerable<Point16> GetNeighbors(Point16 p)
        {
            yield return new Point16(p.X + 1, p.Y);
            yield return new Point16(p.X - 1, p.Y);
            yield return new Point16(p.X, p.Y + 1);
            yield return new Point16(p.X, p.Y - 1);
        }
    }
}