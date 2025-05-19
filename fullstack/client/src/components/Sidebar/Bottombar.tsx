import React from 'react';
import "../styles.css";
import {useAtom} from "jotai";
import {JwtAtom} from "../../atoms/atoms.ts";
import toast from "react-hot-toast";
import {useNavigate} from "react-router";
import {SignInRoute} from "../../helpers/routeConstants.tsx";

export const Bottombar = () => {
    const [jwt, setJwt] = useAtom(JwtAtom);
    const navigate = useNavigate();
    return (
        <div className="flex sticky top-[calc(100vh_-_48px_-_16px)]
         flex-col h-12 border-t px-2 border-[--color-text-grey] justify-end text-xs">
            <div className="flex items-center justify-between">
                <div>
                    <p className="font-bold">Admin</p>
                    <p className="">Administrator</p>
                </div>
                <button className="px-2 py-1.5 bg-[--color-background-grey] hover:bg-[--color-button-grey] transition-colors rounded"
                onClick={() => {
                    localStorage.clear();
                    setJwt("");
                    toast("You have been signed out");
                    navigate(SignInRoute);
                }}>
                    Sign Out
                </button>
            </div>
        </div>
    )
}
