import React from 'react'
import {TopBar} from "./TopBar.tsx";
import {CreationView} from "./CreationView.tsx";

export const CreateService = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4">
            <TopBar/>
            <CreationView/>
        </div>
    )
}
