# Use the original Godot CI image as the base
FROM barichello/godot-ci:4.2.2

# Install X11 libraries required for Godot to run with OpenGL
# Including libxcursor1 to handle cursor imagery
RUN apt-get update && apt-get install -y \
    libx11-6 \
    libxrender1 \
    libgl1-mesa-glx \
    libgl1-mesa-dri \
    libxcursor1 \
    && rm -rf /var/lib/apt/lists/*


