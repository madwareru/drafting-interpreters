using System;
using ReversePolishNotation;

namespace ReversePolishNotationTests;

public class EvaluationTests
{
    [Test]
    public void PassingNullReferencesLeadToNullReferenceException()
    {
        IOperation[] test = null;
        Assert.Catch<NullReferenceException>(() => Operation.Eval(test));
        Assert.Catch<NullReferenceException>(() => Operation.Eval(null));
        Assert.Catch<NullReferenceException>(() => Operation.Eval(Operation.Put(10), Operation.Put(20), null));
    }
    
    [Test]
    public void InsufficientStackSizeLeadsToInvalidOperationException()
    {
        Assert.Catch<InvalidOperationException>(() => Operation.Eval());
        Assert.Catch<InvalidOperationException>(() => Operation.Eval(Operation.Put(10), Operation.Add));
        Assert.Catch<InvalidOperationException>(() => Operation.Eval(Operation.Put(10), Operation.Div));
        Assert.Catch<InvalidOperationException>(() => Operation.Eval(Operation.Add));
        Assert.Catch<InvalidOperationException>(() => Operation.Eval(Operation.Div));
        Assert.Catch<InvalidOperationException>(() => Operation.Eval(Operation.Sqrt));
    }
    
    [Test]
    public void HappyCasesWorkAsExpected()
    {
        Assert.That(7, Is.EqualTo(Operation.Eval(Operation.Put(3), Operation.Put(4), Operation.Add)).Within(0.0001));
        Assert.That(4, Is.EqualTo(Operation.Eval(Operation.Put(20), Operation.Put(5), Operation.Div)).Within(0.0001));
        Assert.That(12, Is.EqualTo(Operation.Eval(Operation.Put(144), Operation.Sqrt)).Within(0.0001));
        Assert.That(5, Is.EqualTo(Operation.Eval(Operation.Put(5))).Within(0.0001));
        Assert.That(145, Is.EqualTo(Operation.Eval(Operation.Put(5), Operation.Put(145))).Within(0.0001));
    }
    
    [Test]
    public void DivisionByZeroLeadsToDivideByZeroException()
    {
        Assert.Catch<DivideByZeroException>(() => Operation.Eval(Operation.Put(10), Operation.Put(0), Operation.Div));
    }
    
    [Test]
    public void SqrtOfANegativeNumberLeadsToAnArgumentOutOfRangeException()
    {
        Assert.Catch<ArgumentOutOfRangeException>(() => Operation.Eval(Operation.Put(-5), Operation.Sqrt));
    }
    
}