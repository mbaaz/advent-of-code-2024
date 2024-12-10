﻿namespace MBZ.AdventOfCode.Year2024.Day06;

public static class Day06Extensions
{
    public static (Map, Guard) ParseToDay06Data(this string[] input)
    {
        var data = input.Select(ParseLine).ToList();
        var tiles = data.Select(item => item.Item1).ToArray();
        var guard = data.Select(item => item.Item2).FirstOrDefault(item2 => item2 != null);

        if (guard == null)
        {
            throw new Exception("Guard was not found on the map!");
        }

        var map = new Map(tiles);
        return (map, guard);
    }

    private static (Tile[], Guard?) ParseLine(string input, int rowIndex)
    {
        var data = input.ToCharArray()
            .Select((row, columnIndex) => ParseRepresentation(row, new Point(rowIndex, columnIndex)))
            .ToList()
        ;
        var tiles = data.Select(item => item.Item1).ToArray();
        var guard = data.Select(item => item.Item2).FirstOrDefault(item2 => item2 != null);
        return (tiles, guard);
    }

    private static (Tile, Guard?) ParseRepresentation(char representation, Point position)
    {
        if (representation is '.')
        {
            return (new EmptyTile(), null);
        }

        if (representation is '#')
        {
            return (new ObstacleTile(), null);
        }

        if (representation is '^')
        {
            const Heading guardHeading = Heading.Up;
            var guard = new Guard(position, guardHeading);
            var tile = new EmptyTile();
            tile.MarkAsVisited(guardHeading, out _);
            return (tile, guard);
        }

        if (representation is 'v' or 'V')
        {
            const Heading guardHeading = Heading.Down;
            var guard = new Guard(position, guardHeading);
            var tile = new EmptyTile();
            tile.MarkAsVisited(guardHeading, out _);
            return (tile, guard);
        }

        if (representation is '>')
        {
            const Heading guardHeading = Heading.Right;
            var guard = new Guard(position, guardHeading);
            var tile = new EmptyTile();
            tile.MarkAsVisited(guardHeading, out _);
            return (tile, guard);
        }

        if (representation is '<')
        {
            const Heading guardHeading = Heading.Left;
            var guard = new Guard(position, guardHeading);
            var tile = new EmptyTile();
            tile.MarkAsVisited(guardHeading, out _);
            return (tile, guard);
        }

        throw new Exception("Unknown symbol!");
    }
}