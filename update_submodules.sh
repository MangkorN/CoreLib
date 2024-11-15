#!/bin/bash

# Update submodules to match the commit references in the main repo

echo "Updating submodules to match the parent repository's commit references..."

# Check if submodules are already initialized
if ! git config --get-regexp '^submodule\..*\.url$' >/dev/null; then
    echo "Submodules not initialized. Initializing now..."
    git submodule update --init --recursive
else
    echo "Submodules already initialized. Updating..."
    git submodule update --recursive
fi

echo "Submodules updated successfully!"