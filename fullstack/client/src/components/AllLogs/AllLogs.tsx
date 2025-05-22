import {TopBar} from "./TopBar.tsx";
import SelectOptions from "./SelectOptions.tsx";
import useInitializeData from "../../hooks/useInitializeData.tsx";
export const AllLogs = () => {
    useInitializeData();
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow">
            <TopBar/>
            <SelectOptions/>
        </div>
    )
}
