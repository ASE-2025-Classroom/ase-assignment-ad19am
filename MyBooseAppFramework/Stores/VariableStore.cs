using System;
using System.Collections.Generic;
using MyBooseAppFramework.Interfaces;

namespace MyBooseAppFramework.Stores
{
    /// <summary>
    /// Stores scalar and array variables.
    /// </summary>
    public class VariableStore : IVariableStore
    {
        private readonly Stack<Dictionary<string, double>> _scalars =
            new Stack<Dictionary<string, double>>();

        private readonly Stack<Dictionary<string, double[]>> _arrays =
            new Stack<Dictionary<string, double[]>>();
        public void CreateArray(string name, int size)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Array name cannot be empty.", nameof(name));

            if (size <= 0)
                throw new ArgumentException("Array size must be greater than zero.", nameof(size));

            _arrays.Peek()[name] = new double[size];
        }

        public VariableStore()
        {
            _scalars.Push(new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase));
            _arrays.Push(new Dictionary<string, double[]>(StringComparer.OrdinalIgnoreCase));
        }

        public void PushScope()
        {
            _scalars.Push(new Dictionary<string, double>(_scalars.Peek(), StringComparer.OrdinalIgnoreCase));
            _arrays.Push(new Dictionary<string, double[]>(_arrays.Peek(), StringComparer.OrdinalIgnoreCase));
        }

        public void PopScope()
        {
            if (_scalars.Count <= 1) throw new InvalidOperationException("Cannot pop global scope.");
            _scalars.Pop();
            _arrays.Pop();
        }

        public bool Exists(string name) =>
            _scalars.Peek().ContainsKey(name) || _arrays.Peek().ContainsKey(name);

        public void SetScalar(string name, double value)
        {
            _scalars.Peek()[name] = value;
        }

        public double GetScalar(string name)
        {
            if (!_scalars.Peek().TryGetValue(name, out var v))
                throw new KeyNotFoundException($"Scalar variable '{name}' not found.");
            return v;
        }

        public void DeclareArray(string name, int size)
        {
            CreateArray(name, size);
        }

        public void SetArrayValue(string name, int index, double value)
        {
            var arr = GetArrayInternal(name);
            if (index < 0 || index >= arr.Length)
                throw new IndexOutOfRangeException($"Index {index} out of bounds for array '{name}' (size {arr.Length}).");
            arr[index] = value;
        }

        public double GetArrayValue(string name, int index)
        {
            var arr = GetArrayInternal(name);
            if (index < 0 || index >= arr.Length)
                throw new IndexOutOfRangeException($"Index {index} out of bounds for array '{name}' (size {arr.Length}).");
            return arr[index];
        }

        private double[] GetArrayInternal(string name)
        {
            if (!_arrays.Peek().TryGetValue(name, out var arr))
                throw new KeyNotFoundException($"Array '{name}' not found.");
            return arr;
        }
    }
}
