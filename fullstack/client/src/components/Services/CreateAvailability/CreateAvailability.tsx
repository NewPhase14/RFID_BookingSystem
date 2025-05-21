import React from 'react'
import {CreationView} from "./CreationView.tsx";
import {TopBar} from "./TopBar.tsx";

export const CreateAvailability = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4">
            <TopBar/>
            <CreationView/>
        </div>
    )
}
