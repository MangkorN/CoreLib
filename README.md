# CoreLib for Unity

CoreLib is a reusable library designed for Unity projects. It provides utility scripts and common implementations for runtime, editor tools, and tests, allowing you to streamline development workflows.

---

## **Setting Up CoreLib as a Submodule**

To use CoreLib in your Unity project as a submodule:

1. **Modify or Create `.gitmodules`**  
   Add the following entry to your `.gitmodules` file, adjusting the `path` to where you want CoreLib to be in your Unity directory:
   ```plaintext
   [submodule "Assets/CoreLib"]
       path = Assets/CoreLib
       url = https://github.com/MangkorN/CoreLib.git
   ```
   - **Important:**  
     The `path` must point to a folder under Unity's `Assets` directory, as Unity only recognizes scripts located in `Assets` or its subdirectories.

   - **Naming Explanation:**  
     - `"Assets/CoreLib"` is the **submodule name** (arbitrary and used for reference in `.gitmodules`).
     - `path` determines where the submodule is located in your repository.  
     - **Best Practice:** Use clear and descriptive names aligned with your folder structure. Naming it `"Assets/CoreLib"` is recommended for consistency and clarity in Unity projects.

2. **Clone or Initialize Submodules**  
   After updating `.gitmodules`, initialize and update submodules:
   ```bash
   git submodule update --init --recursive
   ```

---

## **Using Setup and Update Scripts**

To simplify submodule management and maintenance, add the following scripts to the root directory of your repository (not necessarily the Unity project root).

### **Purpose of These Scripts**

- These scripts are **not an alternative to the initial submodule setup** (i.e., creating or modifying the `.gitmodules` file).
- Instead, they help **initialize, update, and maintain submodules** after the `.gitmodules` configuration has been set up in the repository.

---

### **1. setup_submodules.sh**
- **Purpose:**  
  - Ensures all submodules are initialized and updated to match the commit references in the main repository.
  - Creates a `post-merge` Git hook to automatically update submodules after every `git pull` that causes a merge.

- **Usage:**  
  After cloning the repository or modifying `.gitmodules`, run:
  ```bash
  ./setup_submodules.sh
  ```
  This will:
  - Initialize submodules if they haven't been initialized.
  - Ensure the submodules are updated to the correct state.
  - Set up a Git hook to handle future updates automatically.

---

### **2. update_submodules.sh**
- **Purpose:**  
  - Updates submodules to match the commit references in the main repository.
  - Can be run manually to sync the submodule state at any time.

- **Usage:**  
  Run this script manually whenever you want to ensure submodules are in sync with the main repository:
  ```bash
  ./update_submodules.sh
  ```
