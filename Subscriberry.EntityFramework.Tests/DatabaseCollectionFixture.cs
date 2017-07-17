using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriberry.EntityFramework.Tests
{
	using Xunit;

	[CollectionDefinition(nameof(DatabaseCollectionFixture))]
	public class DatabaseCollectionFixture : ICollectionFixture<DatabaseFixture>
	{
		// This class has no code, and is never created. Its purpose is simply
		// to be the place to apply [CollectionDefinition] and all the
		// ICollectionFixture<> interfaces.
	}
}
