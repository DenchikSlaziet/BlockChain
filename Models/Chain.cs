using BlockChain.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    /// <summary>
    /// Цепочка блоков
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Все блоки
        /// </summary>
        public List<Block> Blocks { get; private set; }

        /// <summary>
        /// Последний блок
        /// </summary>
        public Block BlockLast { get; private set; }


        /// <summary>
        /// Создание новой цепочки
        /// </summary>
        public Chain()
        {
            Blocks = LoadChainFromDB();

            if (Blocks.Count != 0)
            {
                if (Check())
                {
                    BlockLast = Blocks.Last();
                }
                else
                {
                    throw new Exception("Ошибка чтения БД, нарушена связь блоков!");
                }
            }
            else
            {
                var block = new Block();

                Blocks.Add(block);

                BlockLast = block;
                Save(BlockLast);
            }
        }

        /// <summary>
        /// Добавление блока в цепочку
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="user">Пользователь</param>
        public void Add(string data, string user)
        {
            var block = new Block(data, user, BlockLast);

            Blocks.Add(block);
            BlockLast = block;
            Save(BlockLast);
        }

        /// <summary>
        /// Метод проверки корректности цепочки
        /// </summary>
        /// <returns>true - цепочка корректна, false - цепочка некорректна</returns>
        public bool Check()
        {
            var genesisBlock = Blocks[0];
            var previousHash = genesisBlock.Hash;

            foreach (var block in Blocks.Skip(1))
            {
                var hash = block.PreviousHash;

                if (previousHash != hash)
                {
                    return false;
                }

                previousHash = block.Hash;
            }

            return true;
        }

        /// <summary>
        /// Метод записи блока в БД
        /// </summary>
        /// <param name="block">Сохраняемый блок</param>
        private void Save(Block block)
        {
            using(var context = new MyDbContext())
            {
                context.Blocks.Add(block);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Получение данных из БД
        /// </summary>
        /// <returns>Список блоков данных</returns>
        private List<Block> LoadChainFromDB()
        {
            using (var context = new MyDbContext())
            {
                return context.Blocks.ToList();
            }
        }
    }
}
