try:
    import mlagents
    print("ml-agents already installed")
except ImportError:
    print("Missing mlagents. Run python -m pip install -q mlagents==0.27.0")

env_id = "Roguelike"

# -----------------
# This code is used to close an env that might not have been closed before
try:
  env.close()
except:
  pass
# -----------------

from mlagents_envs.environment import UnityEnvironment

from PIL import Image, ImageOps
import numpy as np



env = UnityEnvironment(file_name="enviroments/MLHades", worker_id=15)
env.reset()

for i in range(100):
    env.step() 

for i in range(7):
    env.step() 
    obs = env._env_state[list(env._env_state)[0]][0].obs[1][0]
    grid_sensor = obs[:,:,:].astype(np.uint8)*255

    base_array = np.ones((64, 64, 3), dtype=np.uint8)*255

    base_array-=grid_sensor[:,:,[0,0,0]] # erase walls as black

    holes = (grid_sensor[:,:,[6,6,6]]//255)*100
    base_array-=holes # erase holes as gray
    
    base_array[31:33, 31:33] = [0,255,255] # add player in center

    img = ImageOps.flip(Image.fromarray(base_array, 'RGB').rotate(90)).resize((512,512),Image.NEAREST)
    img.save('my.png')     
    img.show()

env.close()