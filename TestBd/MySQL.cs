using MySql.Data.MySqlClient;

namespace TestBd
{
    public class MySQL
	{

		private MySqlConnection connectionDB;

		/// <summary>Подключение к БД </summary>
		/// <param name="connectionString">строка подключения к БД</param>
		public MySQL(string connectionString)
		{
			connectionDB = new MySqlConnection(connectionString);
			open();
		}


		/// <summary>Открываем соединение с БД </summary>
		private void open() => connectionDB.Open();


		/// <summary>закрываем соединение с БД</summary>
		public void Close()
		{
			connectionDB.Close();
			connectionDB.Dispose();
		}


		/// <summary>
		/// Добавление|Обновление в БД (метод не возращает ничего)
		/// </summary>
		/// <param name="query">строка запроса</param>
		public void InsertUpdateBD(string query)
		{
			using (MySqlCommand command = new MySqlCommand(query, connectionDB))
				command.ExecuteNonQuery();

		}

		/// <summary>
		/// Получение значений из БД
		/// </summary>
		/// <param name="query">строка запроса</param>
		/// <param name="splitSumbol">символ-разделитель столбцов (по умл.|)</param>
		/// <param name="endOfline">символ-разделитель строк (по умл. \n)</param>
		/// <returns></returns>
		public string SelectBD(string query, char splitSumbol = '|', char endOfline = '\n')
		{
			using (MySqlCommand command = new MySqlCommand(query, connectionDB))
			{
				using (MySqlDataReader Reader = command.ExecuteReader())
				{
					string line = string.Empty;
					//читаем ответ
					while (Reader.Read())
					{
						//перебираем полученные поля
						for (int i = 0; i < Reader.FieldCount; i++)
						{
							//составляем строку для добавления в таблицу, по количеству полей
							line = line + Reader[i].ToString() + splitSumbol;
						}
						//line = line.TrimEnd(splitSumbol) + Environment.NewLine;    //обрезаем последний разделитель столбцов
						line = line.Substring(0, line.Length - 1) + endOfline;
					}

					line = line.TrimEnd(endOfline);
					return line;
				}

			}

		}

	}
}
