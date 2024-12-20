﻿using System.Reflection;
using System.Text;
using MBZ.AdventOfCode.Core.Solvers;

namespace MBZ.AdventOfCode.Core.Infrastructure;

public class SolverHelper
{
    private readonly ITypeFinder _typeFinder;

    private IDictionary<int, Type> Solvers { get; }
    public string DefinedSolversHumanReadable { get; }
    public bool HasSolvers => Solvers.Any();
    public int LatestDayWithSolver { get; }

    public SolverHelper(ITypeFinder typeFinder)
    {
        _typeFinder = typeFinder;

        Solvers = GetSolvers();
        DefinedSolversHumanReadable = GetDefinedSolverForDays(Solvers.Keys);

        LatestDayWithSolver = HasSolvers ? Solvers.Keys.Max() : 0;
    }

    public IDaySolutionImplementation? GetSolverForDay(int day)
    {
        if (!Solvers.TryGetValue(day, out var solverType))
        {
            return null;
        }

        var solver = Activator.CreateInstance(solverType) as IDaySolutionImplementation;
        return solver;
    }

    private IDictionary<int, Type> GetSolvers()
    {
        var solverTypes = _typeFinder.FindClassesOfType<IDaySolutionImplementation>();

        var solverTypesDictionary = new Dictionary<int, Type>();
        foreach (var solverType in solverTypes)
        {
            var attr = solverType.GetCustomAttribute<DaySolutionAttribute>();
            if (attr == null)
                throw new Exception($"""Type "{solverType.Name}" is missing attribute [DaySolution(Day = [NN], IsActive = [True|False])]!""");

            if (attr.IsActive)
            {
                solverTypesDictionary.Add(attr.Day, solverType);
            }
        }

        return solverTypesDictionary;
    }

    private static string GetDefinedSolverForDays(IEnumerable<int> keysInput)
    {
        var keys = keysInput.ToList();
        keys.Sort();
        var keysArr = keys.ToArray();

        var prevKey = 0;
        const string rangeSeparator = "-";
        const string separator = ", ";
        var sb = new StringBuilder();

        for (var i = 0; i < keysArr.Length; i++)
        {
            var key = keysArr[i];
            var isFirstKey = i == 0;
            var isLastKey = i == keysArr.Length - 1;
            var isRangeContinuation = key - prevKey == 1;

            if (isFirstKey)
            {
                // Always add the first key to sequence
                sb.Append(key);

                prevKey = key;
                continue;
            }

            if (isRangeContinuation && !isLastKey)
            {
                // This key is continuation of range sequence
                prevKey = key;
                continue;
            }

            // This key is not part of range sequence
            if (!isRangeContinuation)
            {
                sb.Append(rangeSeparator);
                sb.Append(prevKey);
                sb.Append(separator);
                sb.Append(key);
            }
            else if (isLastKey)
            {
                sb.Append(rangeSeparator);
                sb.Append(key);
            }


            prevKey = key;
        }

        return sb.ToString();
    }
}