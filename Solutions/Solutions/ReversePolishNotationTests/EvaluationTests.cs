using System;
using LanguageDevShared;
using ReversePolishNotation;

namespace ReversePolishNotationTests;

public class EvaluationTests
{
    [Test]
    public void InsufficientStackSizeLeadsToNotEnoughOperandsOnAStackError()
    {
        Assert.That(Operation.Eval() is 
            IResult<double, IError>.Err { Error: IError.FailedToGetResultFromAStack });
        
        Assert.That(Operation.Eval(Operation.Put(10), Operation.Add) is
            IResult<double, IError>.Err { Error: IError.NotEnoughOperandsOnAStack { Expected: 2, Fact: 1 } });
        
        Assert.That(Operation.Eval(Operation.Put(10), Operation.Div) 
            is IResult<double, IError>.Err { Error: IError.NotEnoughOperandsOnAStack { Expected: 2, Fact: 1 } });

        Assert.That(Operation.Eval(Operation.Add)
            is IResult<double, IError>.Err { Error: IError.NotEnoughOperandsOnAStack { Expected: 2, Fact: 0 } });
        
        Assert.That(Operation.Eval(Operation.Div) 
            is IResult<double, IError>.Err { Error: IError.NotEnoughOperandsOnAStack { Expected: 2, Fact: 0 } });
        
        Assert.That(Operation.Eval(Operation.Sqrt) 
            is IResult<double, IError>.Err { Error: IError.NotEnoughOperandsOnAStack { Expected: 1, Fact: 0 } });
    }
    
    [Test]
    public void HappyCasesWorkAsExpected()
    {
        var cases = new[]
        {
            (expected: 7.0, program: new[] { Operation.Put(3), Operation.Put(4), Operation.Add }),
            (expected: 4.0, program: new[] { Operation.Put(20), Operation.Put(5), Operation.Div }),
            (expected: 12.0, program: new[] { Operation.Put(144), Operation.Sqrt }),
            (expected: 5.0, program: new[] { Operation.Put(5) }),
            (expected: 145.0, program: new[] { Operation.Put(5), Operation.Put(145) })
        };

        foreach (var (expected, program) in cases)
        {
            Assert.That(
                Operation.Eval(program) is IResult<double, IError>.Ok ok && Math.Abs(ok.Value - expected) < 0.0001
            );
        }
    }
    
    [Test]
    public void WrongInputsLeadToSufficientErrors()
    {
        Assert.That(Operation.Eval(Operation.Put(10), Operation.Put(0), Operation.Div) 
            is IResult<double, IError>.Err { Error: IError.DivisionByZero });
        
        Assert.That(Operation.Eval(Operation.Put(-5), Operation.Sqrt) 
            is IResult<double, IError>.Err { Error: IError.SqrtOfANegativeNumber });
    }
}