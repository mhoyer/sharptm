// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScopedTest.cs">
//  TMAPI.Net was created collectively by the membership of the tmapinet-discuss mailing list 
//  (https://lists.sourceforge.net/lists/listinfo/tmapinet-discuss) with support by the 
//  tmapi-discuss mailing list (http://lists.sourceforge.net/mailman/listinfo/tmapi-discuss),
//  and is hereby released into the public domain; and comes with NO WARRANTY.
//  
//  No one owns TMAPI.Net: you may use it freely in both commercial and
//  non-commercial applications, bundle it with your software
//  distribution, include it on a CD-ROM, list the source code in a
//  book, mirror the documentation at your own web site, or use it in
//  any other way you see fit.
// </copyright>
// <summary>
//   Defines the ScopedTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

    public class ScopedTest : TMAPITestCase
    {
        #region Tests

        private static void Scope_AddScopeAndUsingInvalidScopeThrowsException(IScoped scoped)
        {
            var scopeSize = (scoped is IVariant) ? scoped.Scope.Count : 0;

            Assert.Equal(scopeSize, scoped.Scope.Count);

            var topicMap = scoped.TopicMap;
            var theme1 = topicMap.CreateTopic();
            var theme2 = topicMap.CreateTopic();

            scoped.AddTheme(theme1);
            scopeSize++;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.False(scoped.Scope.Contains(theme2));

            scoped.AddTheme(theme2);
            scopeSize++;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.True(scoped.Scope.Contains(theme2));

            //  add theme1 again, themes arent't allowed to be added multiple times
            scoped.AddTheme(theme1);

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.True(scoped.Scope.Contains(theme2));

            scoped.RemoveTheme(theme2);
            scopeSize--;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            Assert.True(scoped.Scope.Contains(theme1));
            Assert.False(scoped.Scope.Contains(theme2));

            scoped.RemoveTheme(theme1);
            scopeSize--;

            Assert.Equal(scopeSize, scoped.Scope.Count);
            var e = Assert.Throws<ModelConstraintException>("Using null as scope is not allowed.", () => scoped.AddTheme(null));
            Assert.Equal(scoped, e.Reporter);
        }

        [Fact]
        public void TestAssociation()
        {
            Scope_AddScopeAndUsingInvalidScopeThrowsException(CreateAssociation());
        }

        [Fact]
        public void TestOccurrence()
        {
            Scope_AddScopeAndUsingInvalidScopeThrowsException(CreateOccurrence());
        }

        [Fact]
        public void TestName()
        {
            Scope_AddScopeAndUsingInvalidScopeThrowsException(CreateName());
        }

        [Fact]
        public void TestVariant()
        {
            Scope_AddScopeAndUsingInvalidScopeThrowsException(CreateVariant());
        }

        #endregion
    }
}