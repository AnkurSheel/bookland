import { css } from '@emotion/core';
import React from 'react';
import NavLink from './navLink';

const styles = {
    header: css({
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        background: '#eee',
        padding: '0.5rem calc((100vw - 550px - 0.5rem) / 2)',
        borderBottom: '1px solid #ddd',
    }),
    nav: css({
        marginTop: 0,
    }),
};

const Header = () => (
    <header css={styles.header}>
        <nav css={styles.nav}>
            <NavLink to="/">Home</NavLink>
            <NavLink to="/about">About</NavLink>
        </nav>
    </header>
);

export default Header;
