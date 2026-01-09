using Terraria;
using Terraria.ModLoader;
using Industry.Tiles.Blocks.Multiblocks;
using System.Collections.Generic;
using Industry.Tiles.Blocks.Multiblocks.Content.Recipes;

namespace Industry.Tiles.Blocks.Multiblocks
{
    public abstract class MultiblockEntity<TRecipe> : ModTileEntity
        where TRecipe : IMachineRecipe
    {
        // ===== ОБЯЗАТЕЛЬНО ПЕРЕОПРЕДЕЛЯЕТСЯ =====
        protected abstract MultiblockStructure Structure { get; }
        protected abstract IEnumerable<TRecipe> Recipes { get; }

        // ===== СОСТОЯНИЕ =====
        protected TRecipe currentRecipe;
        protected int progress;

        // ===== ОСНОВНОЙ ЦИКЛ =====
        public override void Update()
        {
            // Проверка мультиблока
            if (!Structure.Check(Position.X, Position.Y, out _))
            {
                Kill(Position.X, Position.Y);
                return;
            }

            UpdateMachine();
        }

        // ===== ЛОГИКА МАШИНЫ =====
        private void UpdateMachine()
        {
            if (!TryGetIO(out Chest input, out Chest output))
            {
                Reset();
                return;
            }

            TRecipe recipe = FindRecipe(input, output);

            if (recipe == null)
            {
                Reset();
                return;
            }

            if (!EqualityComparer<TRecipe>.Default.Equals(recipe, currentRecipe))
            {
                currentRecipe = recipe;
                progress = 0;
            }

            progress++;

            if (progress >= recipe.CraftTime)
            {
                recipe.ConsumeInput(input);
                recipe.ProduceOutput(output);

                SyncChest(input);
                SyncChest(output);

                progress = 0;
            }
        }

        // ===== АБСТРАКТНЫЕ МЕТОДЫ =====

        /// <summary>
        /// Получить входной и выходной сундук
        /// </summary>
        protected abstract bool TryGetIO(out Chest input, out Chest output);

        /// <summary>
        /// Найти подходящий рецепт
        /// </summary>
        protected virtual TRecipe FindRecipe(Chest input, Chest output)
        {
            foreach (var recipe in Recipes)
            {
                if (recipe.CanCraft(input) &&
                    recipe.CanOutputFit(output))
                    return recipe;
            }

            return default;
        }

        // ===== УТИЛИТЫ =====

        protected virtual void Reset()
        {
            currentRecipe = default;
            progress = 0;
        }

        protected static void SyncChest(Chest chest)
        {
            if (Main.netMode != Terraria.ID.NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(
                    Terraria.ID.MessageID.SyncChestItem,
                    -1, -1, null,
                    chest.x, chest.y);
            }
        }
    }
}
