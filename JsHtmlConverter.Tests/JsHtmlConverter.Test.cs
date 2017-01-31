namespace JsHtmlConverter.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class JsHtmlConverter_Test
    {
        [Test]
        public void ToHtml_Simple()
        {
            var jsx = "";
            var expected = "<h1>Hello, world!</h1>";
            var actual = JsHtmlConverter.ConvertToHtml(jsx, "<h1>Hello, world!</h1>");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToHtml_SimpleClass()
        {
            var jsx = @"class AboutUs extends React.Component {

    render() {
        return (
            <div>
                <h1>About us page</h1>
                <hr/>
                <p>This is an example page</p>
            </div>
        );
    }
}";
            var expected = @"<div>
  <h1>About us page</h1>
  <hr />
  <p>This is an example page</p>
</div>";
            var actual = JsHtmlConverter.ConvertToHtml(jsx, "<AboutUs />");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToHtml_RealClass()
        {
            var jsx = @"import React, { PropTypes } from 'react';
import { Link } from 'react-router';
    
export default class Front extends React.Component {

    render() {
        return (
            <div>
                <h1>Page not found</h1>
                <hr/>
                <p>This page was not found</p>
                <Link to=""/"">To main page</Link>
            </div>
        );
    }
}";
            var expected = @"<div>
  <h1>Page not found</h1>
  <hr/>
  <p>This page was not found</p>
  <a>To main page</a>
</div>";
            var actual = JsHtmlConverter.ConvertToHtml(jsx, "<AboutUs />");

            Assert.AreEqual(expected, actual);
        }
    }
}
