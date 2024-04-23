# Use the original Godot CI image as the base
FROM barichello/godot-ci:4.2.2

# Install X11 libraries and xvfb
RUN apt-get update && apt-get install -y \
    libx11-6 \
    libxrender1 \
    libgl1-mesa-glx \
    libgl1-mesa-dri \
    libxcursor1 \
    libxkbcommon0 \
    libxinerama1 \
    libxrandr2 \
    libxi6 \
    xvfb \ 
    && rm -rf /var/lib/apt/lists/*




