#!/bin/bash
n_values=(1000000 10000000 50000000)
m_values=(1 2 4 8 16 32 64 128)
methods=("no_threads" "threads_v1" "threads_v2")
# Iterate over each combination of parameters
for n in "${n_values[@]}"; do
  for m in "${m_values[@]}"; do
    for method in "${methods[@]}"; do
      echo "Running dotnet run  $n $m \"$method\""
      dotnet run $n $m "$method"
    done
  done
done