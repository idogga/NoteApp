﻿using NoteAppModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NoteAppModel.Tests
{
    [TestClass()]
    public class LoggerTests
    {
        [TestMethod()]
        public void WriteTest()
        {
            Logger.GetInstance().Write("test");
            var strings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var str in strings)
                Assert.IsFalse(string.IsNullOrEmpty(str), "лог должен быть");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }

        [TestMethod()]
        public void WriteTest1()
        {
            Logger.GetInstance().Write(new { field1 = 0, field2 = "asdfghj" });
            var strings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var str in strings)
                Assert.IsFalse(string.IsNullOrEmpty(str), "лог должен быть");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }

        [TestMethod()]
        public void WriteTest2()
        {
            Logger.GetInstance().Write(new Exception("Тестовая ошибка"));
            var strings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Errors-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var str in strings)
                Assert.IsFalse(string.IsNullOrEmpty(str), "лог должен быть");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Errors-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }

        [TestMethod()]
        public void WriteTestMultiThread()
        {
            var tasks = new List<Task>();
            for(int i = 0; i<20;i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                Logger.GetInstance().Write("test")));
            }
            Task.WaitAll(tasks.ToArray());
            var strings = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            foreach (var str in strings)
                Assert.IsFalse(string.IsNullOrEmpty(str), "лог должен быть");
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
        }
    }
}