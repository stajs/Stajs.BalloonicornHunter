# Stajs.BalloonicornHunter

## Background

Let's say that there is [this game that you like playing](http://www.teamfortress.com/); let's call it TF2. And this game has a [bunch of achievements](http://steamcommunity.com/stats/TF2/achievements/) that you are trying to, uh, achieve. Let us also say, that [one of those achievements](http://wiki.teamfortress.com/w/index.php?title=The_Great_Deflate&redirect=no) involves killing players that have a [certain item](http://wiki.teamfortress.com/wiki/Balloonicorn) equipped. Let us further posit, that you have spent more hours than you care to publically announce, bouncing from game server to game server looking for players, as outlined above, who have that aforementioned item equipped, so that you can thenceforth kill them to advance your progress in said [achievement](http://wiki.teamfortress.com/wiki/Balloonicorn#Related_achievements).

If that hypothetical situation did arise, you might become motivated enough to investigate automating the search for players that match that criteria.

## Prerequisites

### Building

Visual Studio 2012

## Features

### Valve Master Servers

Query the master servers and get a bunch of game servers back.

* Restrict results by region and filters (e.g. game, map, etc). These are hard-coded to look for TF2 game servers.
* Multi-packet responses are not supported.

### Game Servers

#### Info

Query a game server and get server info back.

* Mostly complete responses, but some details (e.g. extra data) are ignored for now.
* Multi-packet responses are not supported.

## Usage

Uh, there is a command line app...? But really, you should be running this out of Visual Studio if you want to see things work.

## Roadmap

Perhaps an actual front-end?