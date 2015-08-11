using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Tetris
{
    class SaveSystem
    {
        public static List<ScoreEntry> Scores = new List<ScoreEntry>();
        public string SaveFilePath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "scores.xml"));
        public string SaveDirectoryPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "C#_Tetris"));
        public SaveSystem()
        {
            if (!Directory.Exists(SaveDirectoryPath))
            {
                Directory.CreateDirectory(SaveDirectoryPath);
            }
            if (!File.Exists(SaveFilePath))
            {
                File.Create(SaveFilePath).Close();
                XElement scores = new XElement("scores", String.Empty);
                scores.Save(SaveFilePath);
            }
        }
        public void ReadHighscore()
        {
            XElement SaveFile = XElement.Load(SaveFilePath);
            var scoreList = from entry in SaveFile.Descendants("score")
                            select new ScoreEntry
                            {
                                Name = (string)entry.Element("name"),
                                Score = (int)entry.Element("value")
                            };
            foreach (ScoreEntry entry in scoreList)
            {
                Scores.Add(entry);
            }
            Scores.Sort();
            Scores.Reverse();
        }
        public void UpdateHighscore()
        {
            string name = Program.CurrentName;
            int score = Program.CurrentScore;
            ScoreEntry entryWithSameName = Scores.Find(e => e.Name == name);            
            ScoreEntry lastEntry = Scores.LastOrDefault();
            if (entryWithSameName != null)
            {
                if (score > entryWithSameName.Score)
                {
                    entryWithSameName.Score = score;
                }
            }
            else
            {
                Scores.Add(new ScoreEntry(name, score));
            }
            Scores.Sort();
            Scores.Reverse();
            if (Scores.Count > 10)
            {
                Scores.RemoveAt(Scores.Count - 1);
            }
            XElement root = new XElement("scores");
            foreach (ScoreEntry entry in Scores)
            {
                XElement node = new XElement("score");
                node.Add(new XElement("name", entry.Name));
                node.Add(new XElement("value", entry.Score));
                root.Add(node);
            }
            root.Save(SaveFilePath);
        }
    }
    public class ScoreEntry : IComparable<ScoreEntry>
    {
        public string Name;
        public int Score;
        public ScoreEntry() { }
        public ScoreEntry(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public int CompareTo(ScoreEntry other)
        {
            if (Score == other.Score)
            {
                return Name.CompareTo(other.Name);
            }
            return Score.CompareTo(other.Score);
        }
    }
}
