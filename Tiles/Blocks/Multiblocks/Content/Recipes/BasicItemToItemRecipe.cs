using System;
using System.Collections.Generic;
using Terraria;

namespace Industry.Tiles.Blocks.Multiblocks.Content.Recipes
{
    public class BasicItemToItemRecipe : IMachineRecipe
    {
        public Dictionary<int, int> Inputs { get; } = new();

        public int OutputItem { get; init; }
        public int OutputStack { get; init; } = 1;

        public int CraftTime { get; init; } = 60;

        // =====================
        // ПРОВЕРКИ
        // =====================

        public bool CanCraft(Chest chest)
        {
            foreach (var req in Inputs)
            {
                int count = 0;

                for (int i = 0; i < 40; i++)
                {
                    Item item = chest.item[i];
                    if (item.type == req.Key)
                        count += item.stack;
                }

                if (count < req.Value)
                    return false;
            }

            return true;
        }

        public bool CanOutputFit(Chest chest)
        {
            for (int i = 0; i < 40; i++)
            {
                Item item = chest.item[i];

                if (item.IsAir)
                    return true;

                if (item.type == OutputItem &&
                    item.stack + OutputStack <= item.maxStack)
                    return true;
            }

            return false;
        }

        // =====================
        // ИЗМЕНЕНИЕ СУНДУКОВ
        // =====================

        public void ConsumeInput(Chest chest)
        {
            foreach (var req in Inputs)
            {
                int need = req.Value;

                for (int i = 0; i < 40 && need > 0; i++)
                {
                    Item item = chest.item[i];
                    if (item.type != req.Key)
                        continue;

                    int take = Math.Min(item.stack, need);
                    item.stack -= take;
                    need -= take;

                    if (item.stack <= 0)
                        item.TurnToAir();
                }
            }
        }

        public void ProduceOutput(Chest chest)
        {
            for (int i = 0; i < 40; i++)
            {
                Item item = chest.item[i];

                if (item.IsAir)
                {
                    item.SetDefaults(OutputItem);
                    item.stack = OutputStack;
                    return;
                }

                if (item.type == OutputItem)
                {
                    item.stack += OutputStack;
                    return;
                }
            }
        }
    }
}
