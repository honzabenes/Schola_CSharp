using ExcelFramework;

namespace Excel_UnitTests
{
    public class Sheet_UnitTests
    {
        [Fact]
        public void AddCell_EmptyCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new EmtpyCell() }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void AddCell_NumberCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "6";

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new NumberCell(6) }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void AddCell_FormulaCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "=A2+D1";

            char @operator = '+';
            var op1address = new CellAddress(0, 1);
            var op2address = new CellAddress(3, 0);

            // Act
            sheet.AddCell(address, content);

            // Assert
            Dictionary<CellAddress, Cell> expected = new()
            {
                { new CellAddress(1, 3), new FormulaCell(@operator, op1address, op2address) }
            };

            Assert.Equal(expected, sheet.Cells);
        }


        [Fact]
        public void GetCellValue_EmptyCell()
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            sheet.AddCell(address, content);

            // Act
            int value = sheet.GetCellValue(address);

            // Assert
            int expected = 0;

            Assert.Equal(expected, value);
        }


        [Fact]
        public void GetCellValue_NumberCell() // TODO
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            sheet.AddCell(address, content);

            // Act
            int value = sheet.GetCellValue(address);

            // Assert
            int expected = 0;

            Assert.Equal(expected, value);
        }


        [Fact]
        public void GetCellValue_FormulaCell() // TODO
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            sheet.AddCell(address, content);

            // Act
            int value = sheet.GetCellValue(address);

            // Assert
            int expected = 0;

            Assert.Equal(expected, value);
        }

        [Fact]
        public void GetCellValue_CellAddressNotInSheet() // TODO
        {
            // Arrange
            var sheet = new Sheet();
            var address = new CellAddress(1, 3);
            string content = "[]";

            sheet.AddCell(address, content);

            // Act
            int value = sheet.GetCellValue(address);

            // Assert
            int expected = 0;

            Assert.Equal(expected, value);
        }
    }
}
