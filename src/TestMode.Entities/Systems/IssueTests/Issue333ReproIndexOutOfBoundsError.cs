﻿// SampSharp
// Copyright 2022 Tim Potze
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using SampSharp.Entities;
using SampSharp.Entities.SAMP;
using SampSharp.Entities.SAMP.Commands;

namespace TestMode.Entities.Systems.IssueTests;

public class Issue333ReproIndexOutOfBoundsError : ISystem
{
    [PlayerCommand]
    public void EntityManagerBugCommand(Player player, IDialogService dialogService)
    {
        // test for issue #333 
        var dialog = new MessageDialog("Welcome", "Welcome on my server!", "Continue");

        dialogService.Show(player.Entity, dialog, async _ =>
        {
            var dialog2 = new InputDialog
            {
                IsPassword = false,
                Caption = "Mail",
                Content = "Please enter your email :",
                Button1 = "Valid",
                Button2 = "Exit"
            };

            var result = await dialogService.Show(player.Entity, dialog2);

            player.SendClientMessage($"You entered {result.InputText}");
        });
    }
}