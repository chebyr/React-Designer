namespace JsHtmlConverter.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class JsHtmlConverter_Test
    {
        [Test]
        public void JsHtmlConverter_Simple()
        {
            var jsx = "";
            var expected = "";
            var actual = JsHtmlConverter.ConvertToHtml(jsx, "<h1>Hello, world!</h1>");

            Assert.AreEqual(expected, actual);
        }
    }
}
