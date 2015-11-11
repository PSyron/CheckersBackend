using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Checkers.Interfaces;
namespace Checkers.Services
{
    /// <summary>
    /// Serwis odpowiedzialny za zapisywanie logow ruchów do bazy danych.
    /// </summary>
    public class Logger : ILogger
    {
        public void DoWork()
        {
        }
    }
}
