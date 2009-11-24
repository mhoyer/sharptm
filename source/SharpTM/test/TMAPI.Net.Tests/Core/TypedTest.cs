// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypedTest.cs">
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
//   Defines the TypedTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TMAPI.Net.UnitTests.Core
{
    using Net.Core;
    using Xunit;

    public class TypedTest : TMAPITestCase
    {
        #region Tests
        private void Type_SetTypeAndUsingInvalidTypeThrowsException(ITyped typed)
        {
            var type = CreateTopic();

            var oldType = typed.Type;

            Assert.NotNull(oldType);

            typed.Type = type;

            Assert.Equal(type, typed.Type);

            typed.Type = oldType;

            Assert.Equal(oldType, typed.Type);
            var e = Assert.Throws<ModelConstraintException>("Using null for type is not allowed.", () => typed.Type = null);
            Assert.Equal(typed, e.Reporter);
        }

        [Fact]
        public void TestAssociation()
        {
            var association = CreateAssociation();

            Type_SetTypeAndUsingInvalidTypeThrowsException(association);
        }

        [Fact]
        public void TestRole()
        {
            var role = CreateRole();

            Type_SetTypeAndUsingInvalidTypeThrowsException(role);
        }

        [Fact]
        public void TestOccurrrence()
        {
            var occurrence = CreateOccurrence();

            Type_SetTypeAndUsingInvalidTypeThrowsException(occurrence);
        }

        [Fact]
        public void TestName()
        {
            var name = CreateName();

            Type_SetTypeAndUsingInvalidTypeThrowsException(name);
        }
        #endregion
    }
}