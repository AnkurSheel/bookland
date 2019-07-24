import { Link, useStaticQuery, graphql } from 'gatsby';
import React from 'react';
import BackgroundImage from 'gatsby-background-image';
import styled from '@emotion/styled';

const ImageBackground = styled(BackgroundImage)`
    background-position: top 20% center;
    background-size: cover;
    height: 30vh;
    + * {
        margin-top: 0;
    }
`;

const TextBox = styled('div')`
    background-image: linear-gradient(to top, #ddbbffdd 2rem, #ddbbff00);
    display: flex;
    flex-direction: column;
    height: 100%;
    justify-content: flex-end;
    padding: 0 calc((100vw - 550px) / 2) 2rem;
    width: 100%;
    height: 30vh;
    + * {
        margin-top: 0;
    }

    h1 {
        text-shadow: 1px 1px 3px #eeddff66;
        font-size: 2.25rem;
    }

    p,
    a {
        color: #222;
        margin-top: 0;
    }

    a {
        margin-top: 0.5rem;
    }
`;
const Hero = () => {
    const { image } = useStaticQuery(graphql`
        query {
            image: file(relativePath: { eq: "inaki-del-olmo-bookshelf.jpg" }) {
                sharp: childImageSharp {
                    fluid {
                        ...GatsbyImageSharpFluid_withWebp
                    }
                }
            }
        }
    `);
    return (
        <ImageBackground Tag="section" fluid={image.sharp.fluid} fadeIn="soft">
            <TextBox>
                <h1>Home</h1>
                <p>What a world.</p>
                <Link to="/about">&rarr; Learn about me</Link>
            </TextBox>
        </ImageBackground>
    );
};

export default Hero;
