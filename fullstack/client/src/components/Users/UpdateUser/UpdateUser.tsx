import React from 'react'
import {TopBar} from "./TopBar.tsx";
import {UpdateView} from "./UpdateView.tsx";

export const UpdateUser = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4">
            <TopBar/>
            <UpdateView/>
        </div>
    )
}
