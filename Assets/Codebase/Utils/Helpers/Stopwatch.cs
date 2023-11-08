using Cysharp.Threading.Tasks;
using System.Threading;

namespace Assets.Codebase.Utils.Helpers
{
    public class Stopwatch
    {
        private float _elapsedTime = 0f;
        private CancellationTokenSource _cancellationToken;
        private bool _isCounting = false;

        public void Start()
        {
            CancelCounting();
            _elapsedTime = 0f;
            _cancellationToken = new CancellationTokenSource();
            _isCounting = true;
            CountTime(_cancellationToken.Token).Forget();
        }

        public float GetElapsedTime(bool stop)
        {
            if (stop)
            {
                _isCounting = false;
                CancelCounting();
            }

            return _elapsedTime;
        }

        private async UniTask CountTime(CancellationToken cancellationToken)
        {
            while (_isCounting)
            {
                await UniTask.Delay(1000, false, PlayerLoopTiming.Update, cancellationToken);
                _elapsedTime++;
            }
        }

        private void CancelCounting()
        {
            if (_cancellationToken != null)
            {
                _cancellationToken.Cancel();
                _cancellationToken.Dispose();
                _cancellationToken = null;
            }
        }
    }
}
