﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteParser
{
    public struct PauseToken
    {
        private readonly PauseTokenSource _source;
        internal PauseToken(PauseTokenSource source) { _source = source; }

        public bool IsPaused => _source != null && _source.IsPaused;

        public Task WaitWhilePausedAsync()
        {
            return IsPaused ?
                _source.WaitWhilePausedAsync() :
                PauseTokenSource.CompletedTask;
        }
    }
}
