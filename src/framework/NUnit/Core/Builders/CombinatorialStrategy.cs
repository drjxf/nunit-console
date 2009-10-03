﻿// ***********************************************************************
// Copyright (c) 2008 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections;
using System.Reflection;
using NUnit.Core.Extensibility;

namespace NUnit.Core.Builders
{
    public class CombinatorialStrategy : CombiningStrategy
    {
        public CombinatorialStrategy(IEnumerable[] sources) : base(sources) { }

        public override IEnumerable GetTestCases()
        {
            IEnumerator[] enumerators = new IEnumerator[Sources.Length];
            int index = -1;

			ArrayList testCases = new ArrayList();

            for (; ; )
            {
                while (++index < Sources.Length)
                {
                    enumerators[index] = Sources[index].GetEnumerator();
                    if (!enumerators[index].MoveNext())
						return testCases;
                }

                object[] testdata = new object[Sources.Length];

                for (int i = 0; i < Sources.Length; i++)
                    testdata[i] = enumerators[i].Current;

				testCases.Add(testdata);

                index = Sources.Length;

                while (--index >= 0 && !enumerators[index].MoveNext()) ;

                if (index < 0) break;
            }

			return testCases;
        }
    }
}