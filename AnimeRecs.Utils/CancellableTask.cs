﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AnimeRecs.Utils
{
    public struct CancellableTask : ICancellableTask
    {
        public Task Task { get; private set; }
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        public CancellableTask(Task task, CancellationTokenSource cancellationTokenSource)
        {
            Task = task;
            CancellationTokenSource = cancellationTokenSource;
        }

        public void Cancel()
        {
            CancellationTokenSource.Cancel();
        }
    }

    public struct CancellableTask<T> : ICancellableTask
    {
        public Task<T> Task { get; private set; }
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        Task ICancellableTask.Task { get { return Task; } }

        public CancellableTask(Task<T> task, CancellationTokenSource cancellationTokenSource)
        {
            Task = task;
            CancellationTokenSource = cancellationTokenSource;
        }

        public void Cancel()
        {
            CancellationTokenSource.Cancel();
        }
    }
}

// Copyright (C) 2017 Greg Najda
//
// This file is part of AnimeRecs.Utils
//
// AnimeRecs.Utils is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// AnimeRecs.Utils is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with AnimeRecs.Utils.  If not, see <http://www.gnu.org/licenses/>.