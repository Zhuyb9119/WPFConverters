using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Xunit;
using Kent.Boogaart.Converters;

namespace Kent.Boogaart.Converters.UnitTest
{
    public sealed class CaseConverterTest : UnitTest
    {
        private CaseConverter _caseConverter;

        protected override void SetUpCore()
        {
            base.SetUpCore();
            _caseConverter = new CaseConverter();
        }

        [Fact]
        public void Constructor_ShouldSetDefaults()
        {
            Assert.Equal(CharacterCasing.Normal, _caseConverter.Casing);
        }

        [Fact]
        public void Constructor_Casing_ShouldSetCasing()
        {
            _caseConverter = new CaseConverter(CharacterCasing.Upper);
            Assert.Equal(CharacterCasing.Upper, _caseConverter.Casing);
            _caseConverter = new CaseConverter(CharacterCasing.Lower);
            Assert.Equal(CharacterCasing.Lower, _caseConverter.Casing);
        }

        [Fact]
        public void Casing_ShouldThrowIfInvalid()
        {
            var ex = Assert.Throws<ArgumentException>(() => _caseConverter.Casing = (CharacterCasing) 100);
            Assert.Equal("'100' is not a valid value for property 'Casing'.", ex.Message);
        }

        [Fact]
        public void Casing_ShouldGetAndSetCasing()
        {
            Assert.Equal(CharacterCasing.Normal, _caseConverter.Casing);
            _caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal(CharacterCasing.Upper, _caseConverter.Casing);
            _caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal(CharacterCasing.Lower, _caseConverter.Casing);
        }

        [Fact]
        public void Convert_ShouldDoNothingIfValueIsNotAString()
        {
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.Convert(123, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.Convert(123d, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.Convert(DateTime.Now, null, null, null));
        }

        [Fact]
        public void Convert_ShouldDoNothingIfCasingIsNormal()
        {
            Assert.Equal(CharacterCasing.Normal, _caseConverter.Casing);
            Assert.Equal("abcd", _caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("ABCD", _caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("AbCd", _caseConverter.Convert("AbCd", null, null, null));
        }

        [Fact]
        public void Convert_ShouldConvertStringsToSpecifiedCasing()
        {
            _caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal("abcd", _caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("abcd", _caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("abcd", _caseConverter.Convert("AbCd", null, null, null));

            _caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal("ABCD", _caseConverter.Convert("abcd", null, null, null));
            Assert.Equal("ABCD", _caseConverter.Convert("ABCD", null, null, null));
            Assert.Equal("ABCD", _caseConverter.Convert("AbCd", null, null, null));
        }

        [Fact]
        public void Convert_ShouldUseSpecifiedCulture()
        {
            CultureInfo cultureInfo = new CultureInfo("tr");

            _caseConverter.Casing = CharacterCasing.Lower;
            Assert.Equal("ijk", _caseConverter.Convert("ijk", null, null, cultureInfo));
            Assert.Equal("ıjk", _caseConverter.Convert("IJK", null, null, cultureInfo));
            Assert.Equal("ijk", _caseConverter.Convert("iJk", null, null, cultureInfo));

            _caseConverter.Casing = CharacterCasing.Upper;
            Assert.Equal("İJK", _caseConverter.Convert("ijk", null, null, cultureInfo));
            Assert.Equal("IJK", _caseConverter.Convert("IJK", null, null, cultureInfo));
            Assert.Equal("İJK", _caseConverter.Convert("iJk", null, null, cultureInfo));
        }

        [Fact]
        public void ConvertBack_ShouldReturnUnsetValue()
        {
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(null, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(123, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.ConvertBack(DateTime.Now, null, null, null));
            Assert.Same(DependencyProperty.UnsetValue, _caseConverter.ConvertBack("abc", null, null, null));
        }
    }
}
