using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public static class DBManager
{
    public static string conn = "URI=file:" + Application.dataPath + "/Plugins/LIBRARYPLAYER.db";

    public static int level;

    public static void CreateDB()
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS playerData (Id INT, Level INT, MaxHealth INT, PositionX TEXT, PositionY TEXT, CountBombs INT, CountPotions INT, HasPowerBombs INT);";
                command.ExecuteNonQuery();
            }

            connection.Clone();
        }
    }

    public static void InsertPlayerData()
    {
        IDbConnection dbconn;

        dbconn = (IDbConnection)new SqliteConnection(conn);

        // DataUnity
        level = 2;

        // SQL
        dbconn.Open();
        using (var dbcmd = dbconn.CreateCommand())
        {
            // Colocar 8 informaciones.
            dbcmd.CommandText = "INSERT INTO playerData (Id, Level,) VALUES ( 1 , '" + level + "' );";
            dbcmd.ExecuteNonQuery();
        }

        dbconn.Close();
    }

    // Se usa para Actualizar los datos del player y meterlo en la base de datos
    
    public static void UpdatePlayerData(PlayerCheckPoint player)
    {
        // DataUnity
        level = player.level;

        // SQL
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Colocar 8 informaciones.
                string query = "UPDATE playerData SET Id= 1, Level= '" + level +  "'  WHERE Id= 1 ;";

                command.CommandText = query;
                command.ExecuteNonQuery();


            }

            connection.Close();
        }

    }
    
    public static void ResetPlayerHaveBombs()
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string query = "UPDATE playerData SET HasPowerBombs = " + 0 + " WHERE Id = 1 ;";
                command.CommandText = query;
                command.ExecuteNonQuery();


            }

            connection.Close();
        }

    }

    // Para Recoger la informacion de la base de datos y ser enviado a traves de una clase
    
    public static PlayerData SelectPlayerData(PlayerCheckPoint player )
    {
        PlayerData data = new PlayerData(player);

        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string query = "SELECT * FROM playerData WHERE Id = 1 ;";

                command.CommandText = query;
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    data.level = reader.GetInt32(1);
                    //data.maxHealth = reader.GetInt32(2);
                    //data.position[0] = float.Parse(reader.GetString(3));
                    //data.position[1] = float.Parse(reader.GetString(4));
                    //data.countBombs = reader.GetInt32(5);
                    //data.countPotions = reader.GetInt32(6);
                    //bool algo = reader.GetBoolean(7);
                    //data.hasPowerBombs = reader.GetBoolean(7);
                }

                reader.Close();
                reader = null;
                connection.Close();

            }
        }

        return data;
    }
    
    public static int GetLevelState()
    {
        level = -1;
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string query = "SELECT * FROM playerData WHERE Id = 1 ;";

                command.CommandText = query;
                IDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    level = reader.GetInt32(1);


                }
            }
        }

        return level;
    }

    public static void DeletePlayerData()
    {
        using (var connection = new SqliteConnection(conn))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                string query = "DELETE FROM playerData WHERE Id=1 ;";

                command.CommandText = query;
                command.ExecuteNonQuery();


            }

            connection.Close();

        }



    }

}
