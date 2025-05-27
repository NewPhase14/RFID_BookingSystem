import {TopBar} from "./TopBar.tsx";
import {UpdateView} from "./UpdateView.tsx";

export const UpdateService = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4">
            <TopBar/>
            <UpdateView/>
        </div>
    )
}

