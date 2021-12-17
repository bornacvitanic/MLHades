from dataclasses import dataclass
from typing import Optional

from mlagents.trainers import learn
import numpy as np

@dataclass
class TrainingArguments:
    """
    Class containing all available arguments needed for training.\n
    config_file: Path to the configuration file used for training.
    project_name: The name of the project that is being trained.
    branch: The specific branch of the project.
    iteration: The current interation of training. Should be unique within the same project branch.
    description: A description of the changes made before this training session.
    results_dir: The path to which the results should be stored.
    enviroment_path: The path to the Unity enviroment executable.
    number_of_enviroments: The number of enviroments that should be run in paralel.
    no_graphics: Defines if the instances of the enviroment should render visuals.
    resume: Used to resume a previous training session.
    force: Used to override a previous training session.
    """
    config_file: str
    project_name: str
    branch: Optional[str] = None
    iteration: Optional[int] = None
    description: Optional[str] = None
    results_dir: Optional[str] = None
    enviroment_path: Optional[str] = None
    number_of_enviroments: Optional[int] = None
    no_graphics: Optional[bool] = False
    time_scale: Optional[int] = None
    resume: Optional[bool] = False
    force: Optional[bool] = False
    @property
    def run_id(self) -> str:
        parts = [self.project_name, self.branch, str(self.iteration) if self.iteration else None, self.description]
        return "_".join([s for s in parts if s])
    @property
    def argument_list(self) -> list:
        arguments = []
        arguments.append(self.config_file)
        if(self.results_dir is not None and self.results_dir is not ""):
            arguments.append("--results-dir="+self.results_dir)
        if(self.enviroment_path is not None and self.enviroment_path is not ""):
            arguments.append("--env="+self.enviroment_path)
        if(self.number_of_enviroments is not None and self.number_of_enviroments > 1):
            arguments.append("--num-envs="+str(self.number_of_enviroments))
        if(self.no_graphics):
            arguments.append("--no-graphics")
        else:
            arguments.append("--width=1024")
            arguments.append("--height=720")
        if(self.time_scale is not None and self.time_scale > 0):
            arguments.append("--time-scale="+str(self.time_scale))
        if(self.run_id is not None and self.run_id is not ""):
            arguments.append("--run-id="+self.run_id)
        if(self.resume):
            arguments.append("--resume")
        if(self.force):
            arguments.append("--force")
        return arguments

def main():
    training_arguments = TrainingArguments(
        config_file = "config/imitation/MLHades.yaml",
        project_name = "MLHades",
        branch = "main",
        iteration = 12,
        description = "grid32x32cellsize1x1_andraycastsensor",
        enviroment_path = "enviroments/MLHades",
        number_of_enviroments = 8,
        no_graphics = True,
        time_scale=20,
        resume = False,
        force = False
    )

    options = learn.parse_command_line(training_arguments.argument_list)
    learn.run_cli(options)

if __name__ == "__main__":
    main()