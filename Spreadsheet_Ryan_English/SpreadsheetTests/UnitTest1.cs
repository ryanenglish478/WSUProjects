namespace ExpressionTree
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //Tests constant addition
        [Test]
        public void Test1()
        {
            ExpressionTree tree1 = new ExpressionTree("4+4");
            Assert.AreEqual(8, tree1.Evaluate());
        }
        //Tests unset variable and constant addition
        [Test]
        public void Test2()
        {
            ExpressionTree tree1 = new ExpressionTree("A1+5");
            Assert.AreEqual(5, tree1.Evaluate());
        }
        //Test two unset variable addition
        [Test]
        public void Test3()
        {
            ExpressionTree tree1 = new ExpressionTree("A1+A1");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Tests constant multiplication
        [Test]
        public void Test4()
        {
            ExpressionTree tree1 = new ExpressionTree("4*4");
            Assert.AreEqual(16, tree1.Evaluate());
        }
        //Tests unset variable and constant multiplication
        [Test]
        public void Test5()
        {
            ExpressionTree tree1 = new ExpressionTree("A1*5");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Test two unset variable multiplication
        [Test]
        public void Test6()
        {
            ExpressionTree tree1 = new ExpressionTree("A1*A1");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Tests constant subtraction
        [Test]
        public void Test7()
        {
            ExpressionTree tree1 = new ExpressionTree("4-4");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Tests unset variable and constant subtraction
        [Test]
        public void Test8()
        {
            ExpressionTree tree1 = new ExpressionTree("A1-5");
            Assert.AreEqual(-5, tree1.Evaluate());
        }
        //Test two unset variable subtraction
        [Test]
        public void Test9()
        {
            ExpressionTree tree1 = new ExpressionTree("A1-A1");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Tests constant division
        [Test]
        public void Test10()
        {
            ExpressionTree tree1 = new ExpressionTree("4/2");
            Assert.AreEqual(2, tree1.Evaluate());
        }
        //Tests unset variable and constant division
        [Test]
        public void Test11()
        {
            ExpressionTree tree1 = new ExpressionTree("A1/5");
            Assert.AreEqual(0, tree1.Evaluate());
        }
        //Tests creation of addition node
        [Test]
        public void Test12()
        {
            OperatorNodeFactory opNode = new OperatorNodeFactory();
            Assert.IsInstanceOf<AdditionOperatorNode>(opNode.CreateOperatorNode('+'));
        }
        //Tests creation of subtraction node
        [Test]
        public void Test13()
        {
            OperatorNodeFactory opNode = new OperatorNodeFactory();
            Assert.IsInstanceOf<SubtractionOperatorNode>(opNode.CreateOperatorNode('-'));
        }
        //Tests creation of multiplication node
        [Test]
        public void Test14()
        {
            OperatorNodeFactory opNode = new OperatorNodeFactory();
            Assert.IsInstanceOf<MultiplicationOperatorNode>(opNode.CreateOperatorNode('*'));
        }
        //Tests creation of division node
        [Test]
        public void Test15()
        {
            OperatorNodeFactory opNode = new OperatorNodeFactory();
            Assert.IsInstanceOf<DivisionOperatorNode>(opNode.CreateOperatorNode('/'));
        }
        //Tests creation of an unhandled operator
        [Test]
        public void Test16()
        {
            OperatorNodeFactory opNode = new OperatorNodeFactory();
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Unhandled operator"),
                delegate { opNode.CreateOperatorNode('1'); });
        }
        //Tests multiple operators
        [Test]
        public void Test17()
        {
            ExpressionTree tree = new ExpressionTree("1+2+3");
            Assert.AreEqual(6, tree.Evaluate());
        }
        //Tests parens, multiple ops, and different ops
        [Test]
        public void Test18()
        {
            ExpressionTree tree = new ExpressionTree("(2*3)+1");
            Assert.AreEqual(7, tree.Evaluate());
        }
        //Tests multiple sets of parens, and multiplication/division
        [Test]
        public void Test19()
        {
            ExpressionTree tree = new ExpressionTree("(2*3)*(8*5)/6");
            Assert.AreEqual(40, tree.Evaluate());
        }
        //Tests invalid expression sequence
        [Test]
        public void Test20()
        {
            ExpressionTree tree = new ExpressionTree("+-");
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Invalid Sequence"),
                delegate { tree.Evaluate(); });
        }

    }
}