#!/bin/bash

# Initialize and update submodules
echo "Initializing and updating submodules..."
git submodule update --init --recursive

# Set up the post-merge hook for auto-updating submodules
echo "Setting up post-merge hook for submodule updates..."
HOOKS_DIR=".git/hooks"
HOOK_FILE="$HOOKS_DIR/post-merge"

# Check if the hooks directory exists, create it if not
if [ ! -d "$HOOKS_DIR" ]; then
    echo "Hooks directory not found. Creating $HOOKS_DIR..."
    mkdir -p "$HOOKS_DIR"
    if [ $? -ne 0 ]; then
        echo "Error: Failed to create hooks directory. Please check your permissions."
        exit 1
    fi
fi

# Write the post-merge hook
cat > "$HOOK_FILE" << 'EOF'
#!/bin/bash
# Automatically update submodules after a pull that causes a merge
echo "Updating submodules after pull..."
git submodule update --recursive
EOF

# Attempt to set executable permissions
chmod +x "$HOOK_FILE"

# Check if the hook is executable
if [ -x "$HOOK_FILE" ]; then
    echo "Executable permissions successfully set for $HOOK_FILE."
else
    echo "Failed to set executable permissions for $HOOK_FILE."
    echo "To ensure the hook works, please run the following command manually:"
    echo "    chmod +x $HOOK_FILE"
    echo "Alternatively, you can manually update submodules by running:"
    echo "    ./update_submodules.sh"
fi

echo "Setup complete. Submodules will now automatically update after each pull if the hook is executable."