# Hangfire.Sample

This sample imlpements the Hangfire.IO to achieve a queue system for generic work items.

This sample is using Redis as a backplane for Signal R. It's also using Redis as an L2 cache for EF. The only functionality dependant on this so far is Identity 2.0 within the sample web project.

This sample has a "fire and forget" web UI which adds items to the queue with little expense - keeping the UI snappy, and the IIS process free to serve pages rather than do heavy processing.

It has a Console application that hosts an external Hangfire server. You can start as many instances of these as you like, on as many servers as you like - all they need is a connection to the SQL server hangfire is queing tasks to, and the Redis server acting as the Signal R backplane.

As tasks are processed, messages are sent from the console application to all connected Signal R clients via the backplane to inform them their task has been processed.
