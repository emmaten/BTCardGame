namespace CardGameTests
{
    [TestClass]
    public class CardGameTests
    {
        /// <summary>
        /// Test ValidateInput().
        /// Test with single card with incorrect first character.
        /// Should return "Card not recognised".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_FirstCharacterOfSingleCardIncorrect()
        {
            // Arrange.
            string input = "1S";
            string expectedOutput = "Card not recognised";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Single card list where first character is invalid not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with single card with incorrect second character.
        /// Should return "Card not recognised".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_SecondCharacterOfSingleCardIncorrect()
        {
            // Arrange.
            string input = "2B";
            string expectedOutput = "Card not recognised";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Single card list where second character is invalid not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with incorrect card within a list.
        /// Should return "Card not recognised".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_IncorrectCardInList()
        {
            // Arrange.
            string input = "2S,1S";
            string expectedOutput = "Card not recognised";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list containing invalid card not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with list of only duplicates of same card.
        /// Should return "Cards cannot be duplicated".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_DuplicatedCardsList()
        {
            // Arrange.
            string input = "3H,3H";
            string expectedOutput = "Cards cannot be duplicated";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list containing only duplicates of same card not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with list containing duplicates.
        /// Should return "Cards cannot be duplicated".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_DuplicatedCardsInList()
        {
            // Arrange.
            string input = "4D,5D,4D";
            string expectedOutput = "Cards cannot be duplicated";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list containing duplicates not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with list containing more than 2 Jokers.
        /// Should return "A hand cannot contain more than two Jokers".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_MoreThanTwoJokers()
        {
            // Arrange.
            string input = "JK,JK,JK";
            string expectedOutput = "A hand cannot contain more than two Jokers";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list containing more than two Jokers not correctly validated");
        }

        /// <summary>
        /// Test ValidateInput().
        /// Test with list containing invalid characters, e.g. |.
        /// Should return "Invalid input string".
        /// </summary>
        [TestMethod]
        public void TestValidateInput_InvalidInput()
        {
            // Arrange.
            string input = "2S|3D";
            string expectedOutput = "Invalid input string";

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list containing invalid character not correctly validated");
        }

        /// Test ValidateInput().
        /// Test with valid input which may be considered an edge case.
        /// Should return empty string.
        /// </summary>
        [TestMethod]
        public void TestValidateInput_ValidInputEdgeCase()
        {
            // Arrange.
            string input = "2S,  jk,TD   , 8H";
            string expectedOutput = string.Empty;

            // Act.
            string output = CardGame.CardGame.ValidateInput(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Valid edge case for card list not correctly validated");
        }

        /// <summary>
        /// Test CalculateScore().
        /// Test with valid list.
        /// This example should return 200.
        /// </summary>
        [TestMethod]
        public void TestCalculateScore_ValidInputNoJokers()
        {
            // Arrange.
            string input = "TC,TD,JK,TH,TS";
            int expectedOutput = 200;

            // Act.
            int output = CardGame.CardGame.CalculateScore(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list score not correctly calculated");
        }

        /// <summary>
        /// Test CalculateScore().
        /// Test with valid list containing two Jokers.
        /// This example should return 400.
        /// </summary>
        [TestMethod]
        public void TestCalculateScore_ValidInputWithTwoJokers()
        {
            // Arrange.
            string input = "TC,TD,JK,TH,TS,JK";
            int expectedOutput = 400;

            // Act.
            int output = CardGame.CardGame.CalculateScore(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list score not correctly calculated");
        }

        /// Test CalculateScore().
        /// Test with valid list containing only two Jokers.
        /// This example should return 0.
        /// </summary>
        [TestMethod]
        public void TestCalculateScore_ValidInputWithOnlyTwoJokers()
        {
            // Arrange.
            string input = "JK,JK";
            int expectedOutput = 0;

            // Act.
            int output = CardGame.CardGame.CalculateScore(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list score not correctly calculated");
        }

        /// Test CalculateScore().
        /// Test with valid list.
        /// This example should return 102.
        /// </summary>
        [TestMethod]
        public void TestCalculateScore_ValidInput()
        {
            // Arrange.
            string input = "TC,TD,TH,TS,2C";
            int expectedOutput = 102;

            // Act.
            int output = CardGame.CardGame.CalculateScore(input);

            // Assert.
            Assert.AreEqual(expectedOutput, output, "Card list score not correctly calculated");
        }
    }
}