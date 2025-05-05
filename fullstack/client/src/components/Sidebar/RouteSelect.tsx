import React from 'react'
import {IconType} from "react-icons";
import {FiHome, FiUsers} from "react-icons/fi";
import {BsFillDoorOpenFill} from "react-icons/bs";
import "../styles.css";
import { LuLogs } from 'react-icons/lu';

export const RouteSelect = () => {
    return (
        <div className="space-y-1">
            <Route selected={true} Icon={FiHome} title={"Dashboard"} />
            <Route selected={false} Icon={FiUsers} title={"Users"} />
            <Route selected={false} Icon={LuLogs} title={"Logs"} />
            <Route selected={false} Icon={BsFillDoorOpenFill} title={"Rooms"}/>
        </div>
    )
};

const Route = ({selected, Icon, title}: {
    selected: boolean;
    Icon: IconType;
    title: string;
}) => {
    return (
        <button
        className={`flex items-center justify-start gap-2
         w-full rounded px-2 py-1.5 text-sm transition-[box-shadow,_background-color,_color] ${
            selected
                ? "bg-[--color-background-grey] text-white"
                : "hover:bg-[--color-button-grey] bg-transparent text-white/50 shadow-none"
        }`}
        >
        <Icon className={selected ? "text-text-baby-blue" : ""}/>
        <span>{title}</span>

    </button>
    );
};
