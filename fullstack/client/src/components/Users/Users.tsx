import {TopBar} from "./TopBar.tsx";
import {Table} from "./Table.tsx";

export const Users = () => {
    return (
        <div className="bg-background-grey rounded-lg pb-4 shadow h-[200vh]">
            <TopBar/>
            <Table/>
        </div>
    )
}
