using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DatabaseCreator.Tests
{
	[TestFixture]
    public class DatabaseCreatorTests
    {
		private static Process _app;

		[TestFixtureSetUp]
		public void Setup()
		{
			//Given
			_app = new Process();
			_app.StartInfo.FileName = "DatabaseCreator.exe";
			_app.StartInfo.UseShellExecute = false;
			_app.StartInfo.RedirectStandardOutput = true;			
		}

		[Test]
		public void Should_return_useage_string_when_given_no_arguments()
		{
			//When
			_app.Start();
			var output = _app.StandardOutput.ReadToEnd();

			//Then
			Assert.That(output, Is.EqualTo("Usage: DatabaseCreator DatabaseName DatabaseConnectionString\r\n"));
		}

		[Test]
		public void Should_return_error_when_only_given_one_argument()
		{
			//When
			_app.StartInfo.Arguments = " LocalDatabaseName";
			_app.Start();
			var output = _app.StandardOutput.ReadToEnd();

			//Then
			Assert.That(output, Is.EqualTo("Usage: DatabaseCreator DatabaseName DatabaseConnectionString\r\n"));			
		}		
		
		[Test]
		public void Should_return_error_when_only_given_more_than_3_arguments()
		{
			//When
			_app.StartInfo.Arguments = " LocalDatabaseName Another Another AndAnother";
			_app.Start();
			var output = _app.StandardOutput.ReadToEnd();

			//Then
			Assert.That(output, Is.EqualTo("Usage: DatabaseCreator DatabaseName DatabaseConnectionString\r\n"));			
		}
    }
}
