using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Models
{
    /// <summary>
    /// Сущность блока данных
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Данные
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Хэш блока
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Хэш предыдущего блока
        /// </summary>
        public string PreviousHash { get; private set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string User { get; private set; }

        /// <summary>
        /// Конструктор генезис блока
        /// </summary>
        public Block()
        {
            Id = 1;
            Data = "Первый блок";
            Created = DateTime.Now.Date;
            PreviousHash = "111111";
            User = "Admin";
            Hash = GetHash(GetData());
        }

        /// <summary>
        /// Конструктор блока
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="user">Пользователь</param>
        /// <param name="block">Предыдущий блок</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Block(string data, string user, Block block)
        {
            if(data == null)
            {
                throw new ArgumentNullException("Пустой аргумент data");
            }

            if(block == null)
            {
                throw new ArgumentNullException("Пустой аргумент block");
            }

            if(string.IsNullOrWhiteSpace(user))
            {
                throw new ArgumentNullException("Пустой аргумент user");
            }

            Data = data;
            User = user;
            PreviousHash = block.Hash;
            Created = DateTime.Now.Date;
            Id = block.Id+1;
            Hash = GetHash(GetData());
        }

        private string GetData()
        {
            var sb = new StringBuilder();

            sb.Append(Id.ToString());
            sb.Append(Data.ToString());
            sb.Append(Created.ToShortDateString());
            sb.Append(User);
            sb.Append(PreviousHash);

            return sb.ToString();
        }

        private string GetHash(string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            var hashString = new SHA256Managed();
            var hashValue = hashString.ComputeHash(message);
            var sb = new StringBuilder();

            foreach (var item in hashValue)
            {
                sb.Append(String.Format("{0:x2}", item));
            }

            return sb.ToString();  
        }

        public override string ToString()
        {
            return Data;
        }

    }
}
